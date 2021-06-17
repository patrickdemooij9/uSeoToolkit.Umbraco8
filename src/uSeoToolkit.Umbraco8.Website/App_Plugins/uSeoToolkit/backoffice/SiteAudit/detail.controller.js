(function () {
    "use strict";

    function siteAuditDetailController($routeParams, $http, siteAuditHub) {

        var vm = this;

        vm.isLoading = true;
        vm.errors = 0;
        vm.warnings = 0;

        vm.openPage = openPage;
        vm.closePage = closePage;
        vm.toggleShowIssues = toggleShowIssues;
        vm.pageFilter = pageFilter;

        function init() {
            var auditId = $routeParams.id;
            vm.isLoading = true;

            siteAuditHub.initHub(function (hub) {
                vm.hub = hub;

                const clientId = getClientId();
                if (!clientId) {
                    vm.hub.on("update",
                        function (update) {
                            console.log(update);
                            vm.audit = update;
                            loadAudit(false);
                        });

                    vm.hub.start(function () {
                        loadCurrentAudit(auditId);
                    });
                    return;
                }

                loadCurrentAudit(auditId);
            });
        }

        function loadCurrentAudit(id) {
            $http.get("backoffice/uSeoToolkit/SiteAudit/Connect?auditId=" + id + "&clientId=" + getClientId()).then(function (response) {
                vm.audit = response.data;
                vm.isLoading = false;

                loadAudit(true);
            });
        }

        function loadAudit(hideShow) {
            vm.errors = 0;
            vm.warnings = 0;
            vm.audit.PagesCrawled.forEach(function (p) {
                p.Errors = 0;
                p.Warnings = 0;
                p.Show = hideShow ? false : p.Show;
                p.Results.forEach(function (r) {
                    if (r.IsError) {
                        vm.errors++;
                        p.Errors++;
                    }
                    if (r.IsWarning) {
                        vm.warnings++;
                        p.Warnings++;
                    }
                });
            });
        }

        function openPage(page){
            page.Show = true;
        };

        function closePage(page) {
            page.Show = false;
        };

        function toggleShowIssues() {
            vm.onlyShowIssues = !vm.onlyShowIssues;
        };

        function pageFilter(page) {
            if (!vm.onlyShowIssues) {
                return true;
            }
            return page.Errors > 0 || page.Warnings > 0;
        }

        function getClientId() {
            if ($.connection !== undefined && $.connection.hub !== undefined) {
                return $.connection.hub.id;
            }
            return "";
        }

        init();
    }

    angular.module("umbraco").controller("uSeoToolkit.SiteAudit.DetailController", siteAuditDetailController);
})();