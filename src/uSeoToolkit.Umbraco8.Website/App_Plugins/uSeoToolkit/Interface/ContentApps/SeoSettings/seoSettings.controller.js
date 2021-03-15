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