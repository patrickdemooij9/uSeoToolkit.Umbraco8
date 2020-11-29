angular.module("umbraco").component("siteAuditDetail", {
    templateUrl: "/App_Plugins/uSeoToolkit/Interface/SiteAudit/detail.html",
    controller: ["$routeParams", "$http", "eventsService", function ($routeParams, $http, eventsService) {
        this.$onInit = function () {
            var auditId = $routeParams.id;
            if (!auditId) {
                eventsService.emit("uSeoToolkit.ViewUpdate", "SiteAuditOverview");
                return;
            }

            state.isLoading = true;
            fn.loadCurrentAudit(auditId);
        }

        var state = this.state = {
            isLoading: false,
            audit: null
        }

        var fn = this.fn = {
            loadCurrentAudit: function (id) {
                $http.get("backoffice/uSeoToolkit/SiteAudit/Get?id=" + id).then(function (response) {
                    state.audit = response.data;
                    state.isLoading = false;
                });
            }
        }
    }]
});