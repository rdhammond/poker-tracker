angular.module('pokerTracker', [])
    .controller('PokerTracker', ['$scope', '$rootScope', '$http', 'ms', 'actionUrls',
    function ($scope, $rootScope, $http, ms, actionUrls) {
        var local = {
            newTimeEntry: function () {
                return {
                    RecordedAt: null,
                    StackSize: null,
                    DealerTokes: null,
                    ServerTips: null
                };
            }
        };

        $scope.addTimeEntry = function () {
            $session.timeEntry.RecordedAt = ms.date(new Date());
            $scope.session.TimeEntries.push($session.timeEntry);

            $scope.timeEntry = newTimeEntry();
        };

        $rootScope.$on('sessionStarted', function (event, data) {
            $scope.session = data;

            $scope.session.timeEntries.push({
                RecordedAt: ms.date(newDate()),
                StackSize: $scope.session.StartingStackSize,
                DealerTokes: 0,
                ServerTips: 0
            });

            delete $scope.session.StartingStackSize;
            $('#frmTimeEntries').removeClass('hidden');
        });

        $rootScope.$on('sessionSaved', function (event, data) {
            $scope.timeEntry = newTimeEntry();
            $('#frmTimeEntries').removeClass('hidden');
        });

        $rootScope.$on('sessionCanceled', function (event, data) {
            $scope.timeEntry = newTimeEntry();
            $('#frmTimeEntries').removeClass('hidden');
        });

        $scope.timeEntry = newTimeEntry();
    }
]);