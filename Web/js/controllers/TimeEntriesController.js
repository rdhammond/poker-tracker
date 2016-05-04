angular.module('pokerTracker', [])
    .controller('PokerTracker', ['$scope', '$rootScope', '$http', 'ms', 'actionUrls',
    function($scope, $rootScope, $http, ms, actionUrls) {
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

        $scope.saveSession = function () {
            $rootScope.$emit('saveSession', $scope.session);
        };

        $rootScope.$on('sessionStarted', function (event, data) {
            $scope.session = data;
            $('#frmTimeEntries').removeClass('hidden');
        });

        $rootScope.$on('sessionSaved', function (event, data) {
            $scope.timeEntry = newTimeEntry();
            $('#frmTimeEntries').removeClass();
        });

        $scope.timeEntry = newTimeEntry();
    }
])