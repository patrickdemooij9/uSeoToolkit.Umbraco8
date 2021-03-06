﻿(function () {
    "use strict";

    function ContentAppDocumentSettings($scope, $http, notificationsService, editorService) {

        var vm = this;

        vm.loading = true;

        vm.customBaseFields = {};
        vm.customSelectedFields = {};

        vm.toggleSeoSettings = function () {
            vm.model.enableSeoSettings = !vm.model.enableSeoSettings;
        }

        vm.openContentTypeDialog = function () {
            var editor = {
                multiPicker: false,
                filterCssClass: "not-allowed not-published",
                filter: function (item) {
                    return item.nodeType === "container" ||
                        (vm.model.inheritance != null && vm.model.inheritance.id == item.id) ||
                        $scope.model.id == item.id ||
                        !$scope.model.compositeContentTypes.includes(item.alias);
                },
                submit: function (model) {
                    if (model.selection.length > 0) {
                        const item = model.selection[0];
                        vm.model.inheritance = {
                            id: item.id,
                            name: item.name
                        };
                    } else {
                        vm.model.inheritance = null;
                    }

                    editorService.close();
                },
                close: function () {
                    editorService.close();
                }
            }

            editorService.contentTypePicker(editor);
        }

        vm.removeInheritance = function () {
            vm.model.inheritance = null;
        }

        console.log($scope.model);
        console.log($scope.model.groups);

        $scope.$on("formSubmitting",
            function () {
                save();
            });

        function init() {
            $http.get("backoffice/uSeoToolkit/DocumentTypeSettings/Get?nodeId=" + $scope.model.id).then(function (response) {
                vm.loading = false;
                vm.model = response.data.contentModel;
                vm.model.nodeId = $scope.model.id;
            });
        }

        function save() {
            $scope.$broadcast("uSeoToolkit.SaveDocumentType");

            var postModel = {
                nodeId: vm.model.nodeId,
                enableSeoSettings: vm.model.enableSeoSettings,
                fields: Object.assign({},
                    ...vm.model.values.map(function (v) {
                        return ({ [v.alias]: v.value });
                    })),
                inheritanceId: vm.model.inheritance != null ? vm.model.inheritance.id : null
            };

            $http.post("backoffice/uSeoToolkit/DocumentTypeSettings/Save", postModel).then(function (response) {
                if (response.status !== 200) {
                    notificationsService.error("Something went wrong while saving Seo Settings");
                } else {
                    notificationsService.success("Seo Settings saved!");
                }
            });
        }

        init();
    }

    angular.module("umbraco").controller("uSeoToolkit.ContentApps.DocumentSettingController", ContentAppDocumentSettings);
})();