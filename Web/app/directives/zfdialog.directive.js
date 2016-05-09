(function ($) {
    'use strict';

    angular
        .module('app')
        .directive('zfDialog', zfDialog);

    function zfDialog() {
        return {
            restrict: 'E',
            transclude: true,
            replace: true,
            template: '<div class="reveal" data-reveal><div ng-transclude></div>'
                + '<button class="close-button" data-close aria-label="Close modal" type="button">'
                + '<span aria-hide="true">&times;</span>'
                + '</button></div>'
        };
    }
})();