﻿(function () {
    "use strict";

    function siteAuditListController($scope, $http, $location, listViewHelper) {
        var vm = this;

        vm.items = [];
        vm.selection = [];

        vm.options = {
            filter: '',
            orderBy: "name",
            orderDirection: "asc",
            includeProperties: [
                { alias: "name", header: "Name" }
            ],
            bulkActionsAllowed: true
        };

        vm.selectItem = selectItem;
        vm.clickItem = clickItem;
        vm.selectAll = selectAll;
        vm.isSelectedAll = isSelectedAll;
        vm.isSortDirection = isSortDirection;
        vm.sort = sort;
        vm.clearSelection = clearSelection;

        vm.create = create;
        vm.deleteSelection = deleteSelection;

        function selectAll($event) {
            listViewHelper.selectAllItemsToggle(vm.items, vm.selection);
        }

        function isSelectedAll() {
            return listViewHelper.isSelectedAll(vm.items, vm.selection);
        }

        function clickItem(item) {
            listViewHelper.editItem(item, vm);
        }

        function selectItem(selectedItem, $index, $event) {
            listViewHelper.selectHandler(selectedItem, $index, vm.items, vm.selection, $event);
        }

        function isSortDirection(col, direction) {
            return listViewHelper.setSortingDirection(col, direction, vm.options);
        }

        function sort(field, allow, isSystem) {
            if (allow) {
                vm.options.orderBySystemField = isSystem;
                listViewHelper.setSorting(field, allow, vm.options);
            }
        }

        function clearSelection() {
            vm.selection = [];
        }

        function create() {
            $location.path("uSeoToolkit/SiteAudit/create");
        }

        function deleteSelection() {
            vm.loading = true;
            $http.get("backoffice/uSeoToolkit/SiteAudit/Delete",
                {
                    ids: vm.selection.map(function (item) {
                        return item.id;
                    })
                }).then(function (response) {
                    setItems(response.data);
                    vm.loading = false;
                });
        }

        function setItems(items) {
            vm.items = items.map(function (i) {
                return {
                    "id": i.Id,
                    "icon": "icon-document",
                    "name": i.Name,
                    "published": true,
                    "editPath": "uSeoToolkit/SiteAudit/detail?id=" + i.Id
                }
            });
        }

        function init() {
            vm.loading = true;
            $http.get("backoffice/uSeoToolkit/SiteAudit/GetAll").then(function (response) {
                setItems(response.data);
                vm.loading = false;
            });
        }

        init();
    }

    angular.module("umbraco").controller("uSeoToolkit.SiteAudit.ListController", siteAuditListController);
})();