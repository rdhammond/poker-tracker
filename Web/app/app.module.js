(function () {
    'use strict';

    angular
        .module('app', [])
        .run(function() {
            $(function () {
                var dlgFinish = new Foundation.Reveal($('#dlgFinish')),
                    dlgCancel = new Foundation.Reveal($('#dlgCancel'));
            });
        });
})();