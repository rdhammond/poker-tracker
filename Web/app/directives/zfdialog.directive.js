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
            link: link,
            templateUrl: 'views/zf-dialog.html'
        };

        function link(scope, element, attrs) {
            var dialog = new Foundation.Reveal(element);

            element.on('$destroy', function () {
                dialog.foundation('destroy');
            });
        }
    }
})();