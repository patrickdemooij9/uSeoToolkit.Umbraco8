(function () {
    "use strict";

    function siteAuditListController($scope, $http, listViewHelper) {
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

        function init() {
            vm.loading = true;
            $http.get("backoffice/uSeoToolkit/SiteAudit/GetAll").then(function (response) {
                vm.items = response.data.map(function (i) {
                    return {
                        "id": i.Id,
                        "icon": "icon-document",
                        "name": i.Name,
                        "published": true,
                        "editPath": "uSeoToolkit/SiteAudit/detail?id=" + i.Id
                    }
                });
                vm.loading = false;
            });
        }

        init();
    }

    angular.module("umbraco").controller("uSeoToolkit.SiteAudit.ListController", siteAuditListController);
})();