(function () {
    "use strict";

    function SeoSettingsController($scope, $rootScope, $http, editorState) {

        var vm = this;
        vm.loading = true;

        vm.defaultTitleFields = [];
        vm.defaultDescriptionFields = [];

        vm.title = "";
        vm.description = "";

        function init() {
            console.log(editorState.current);

            $http.get("backoffice/uSeoToolkit/SeoSettings/Get?nodeId=" + editorState.current.id + "&contentTypeId=" + editorState.current.contentTypeId).then(
                function (response) {
                    vm.defaultTitleFields = response.data.defaultTitleFields;
                    vm.defaultDescriptionFields = response.data.defaultDescriptionFields;

                    updateMeta();

                    vm.loading = false;
                });
        }

        function updateMeta() {
            vm.title = getValue(vm.defaultTitleFields);
            vm.description = getValue(vm.defaultDescriptionFields);
        }

        function getValue(fields) {
            for (let field of fields) {
                for (let tab of editorState.current.variants.find(x => x.active).tabs) {
                    for (let prop of tab.properties) {
                        if (prop.alias === field) {
                            if (prop.value) {
                                return prop.value;
                            }
                        }
                    }
                }
            }
        }

        $rootScope.$on("app.tabChange",
            (e, data) => {
                if (data.alias !== "seoSettings") {
                    return;
                }
                if (!vm.loading) {
                    updateMeta();
                }
            });

        init();
    }

    angular.module("umbraco").controller("uSeoToolkit.ContentApps.SeoSettingsController", SeoSettingsController);
})();