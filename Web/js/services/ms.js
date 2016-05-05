angular.module('pokerTracker.services', [])
    .factory('ms', [function() {
        return {
            date: function (jsDate) {
                return '/Date(' + jsDate.getTime() + ')/';
            }
        };
    }
]);