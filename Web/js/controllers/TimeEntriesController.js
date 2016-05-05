angular.module('pokerTracker.controllers', ['pokerTracker.services'])
    .controller('PokerTracker', ['$scope', '$rootScope', 'ms', 'TimeEntry', 'actionUrls',
    function ($scope, $rootScope, $http, ms, TimeEntry, actionUrls) {
        $scope.isShown = false;

        $scope.addTimeEntry = function () {
            $scope.timeEntry.RecordedAt = ms.date(new Date());
            $scope.session.TimeEntries.push($session.timeEntry);
            $scope.timeEntry = new TimeEntry();
        };

        $scope.saveSession = function () {
            $rootScope.$emit('save', $scope.session);
        };

        $rootScope.$on('sessionStarted', function (event, data) {
            $scope.session = data;

            var timeEntry = new TimeEntry();
            timeEntry.RecordedAt = ms.date(new Date());
            timeEntry.StackSize = $scope.session.StartingStackSize;
            timeEntry.DealerTokes = 0;
            timeEntry.ServerTips = 0;

            $scope.session.timeEntries.push(timeEntry);
            delete $scope.session.StartingStackSize;
            $scope.isShown = true;
        });

        $rootScope.$on('sessionSaved', function (event, data) {
            $scope.timeEntry = newTimeEntry();
            $scope.isShown = false;
        });

        $rootScope.$on('sessionCanceled', function (event, data) {
            $scope.timeEntry = newTimeEntry();
            $scope.isShown = false;
        });

        $scope.timeEntry = new TimeEntry();
    }
]);