angular.module('pokerTracker', [])
    .factory('ms', [function() {
        return {
            date: function (jsDate) {
                return '/Date(' + jsDate.getTime() + ')/';
            }
        };
    }
]);