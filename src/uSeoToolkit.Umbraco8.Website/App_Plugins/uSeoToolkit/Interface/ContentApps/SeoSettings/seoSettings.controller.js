(function () {
    "use strict";

    function SeoSettingsController($scope, $rootScope, $http, editorState, entityResource) {

        var vm = this;
        vm.loading = true;

        vm.metaValues = {};

        function init() {
            console.log(editorState.current);

            $http.get("backoffice/uSeoToolkit/SeoSettings/Get?nodeId=" + editorState.current.id + "&contentTypeId=" + editorState.current.contentTypeId).then(
                function (response) {
                    vm.fields = response.data.fields;

                    updateMeta();

                    vm.loading = false;
                });
        }

        function updateMeta() {

            vm.fields.forEach(function (f) {
                if (f.values) {
                    var value = getValue(f.values);
                    if (value && value.startsWith('umb://')) {
                        var entityType = value.substring(6, 11);
                        entityResource.getById(value, entityType).then(function(result) {
                            vm.metaValues[f.alias] = result.metaData.MediaPath;
                        });
                    }
                    vm.metaValues[f.alias] = value;
                } else {
                    vm.metaValues[f.alias] = 'Not set';
                }
            });
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