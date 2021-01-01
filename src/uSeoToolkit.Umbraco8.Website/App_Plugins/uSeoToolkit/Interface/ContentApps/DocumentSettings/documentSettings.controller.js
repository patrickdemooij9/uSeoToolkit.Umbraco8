(function () {
    "use strict";

    function ContentAppDocumentSettings($scope, $http, notificationsService) {

        var vm = this;

        vm.loading = true;

        vm.selectedTitleFields = [];
        vm.selectedDescriptionFields = [];

        vm.toggleSeoSettings = function () {
            vm.model.enableSeoSettings = !vm.model.enableSeoSettings;
        }

        vm.save = function () {
            vm.model.defaultTitleFields = vm.selectedTitleFields.map(function (v) {
                return v.value;
            });
            vm.model.defaultDescriptionFields = vm.selectedDescriptionFields.map(function (v) {
                return v.value;
            });

            $http.post("backoffice/uSeoToolkit/DocumentTypeSettings/Save", vm.model).then(function (response) {
                notificationsService.success("Seo Settings saved!");
            });
        }

        console.log($scope.model.groups);

        function init() {
            $http.get("backoffice/uSeoToolkit/DocumentTypeSettings/Get?nodeId=" + $scope.model.id).then(function (response) {
                vm.loading = false;
                vm.model = response.data.contentModel;
                vm.model.nodeId = $scope.model.id;

                vm.titleFields = [];
                vm.descriptionFields = [];

                getAllContentFields(response.data.defaultTitleFieldTypes).forEach(function(field) {
                    if (vm.model.defaultTitleFields && vm.model.defaultTitleFields.includes(field.value)) {
                        vm.selectedTitleFields.push(field);
                    } else {
                        vm.titleFields.push(field);
                    }
                });

                getAllContentFields(response.data.defaultDescriptionFieldTypes).forEach(function (field) {
                    if (vm.model.defaultDescriptionFields && vm.model.defaultDescriptionFields.includes(field.value)) {
                        vm.selectedDescriptionFields.push(field);
                    } else {
                        vm.descriptionFields.push(field);
                    }
                });
            });
        }

        function getAllContentFields(fields) {
            return $scope.model.groups.flatMap(function (g) {
                return g.properties;
            }).filter(function (g) {
                return fields.includes(g.editor);
            }).map(function (g) {
                return { name: g.label, value: g.alias };
            });
        }

        init();
    }

    angular.module("umbraco").controller("uSeoToolkit.ContentApps.DocumentSettingController", ContentAppDocumentSettings);
})();