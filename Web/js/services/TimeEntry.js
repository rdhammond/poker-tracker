angular.module('pokerTracker.services', [])
    .factory('TimeEntry', [function () {
        return function TimeEntry() {
            this.RecordedAt = null;
            this.StackSize = null;
            this.DealerTokens = null;
            this.ServerTips = null;
        };
    }
]);