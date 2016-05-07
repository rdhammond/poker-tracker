(function ($) {
    'use strict';

    angular
        .module('app')
        .directive('zfDialog', zfDialog);

    function zfDialog() {
        return {
            restrict: 'E',
            transclude: true,
            templateUrl: 'zfDialog.html'
        };
    }
})();