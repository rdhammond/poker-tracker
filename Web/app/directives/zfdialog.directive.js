(function () {
    'use strict';

    angular
        .module('app')
        .directive('zfDialog', zfDialog);

    function zfDialog() {
        return {
            restrict: 'E',
            transclude: true,
            replace: true,
            templateUrl: 'views/zf-dialog.html'
        };
    }
})();