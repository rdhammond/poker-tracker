angular.module('pokerTracker', [])
    .controller('SessionController', ['$scope', '$rootScope', '$http', '$ms', 'actionUrls',
    function ($scope, $rootScope, $http, $ms, actionUrls) {
        var local = {
            startNewSession: function () {
                $http.get(urlActions.createSession).success(function (data) {
                    data.StackSize = null;
                    $scope.session = data;
                    $('#frmSession').removeClass('hidden');
                });
            }
        };

        $scope.startSession = function () {
            $rootScope.$emit('sessionStarted', $scope.session);
            $('#frmSession').addClass('hidden');
        };

        $rootScope.$on('saveSession', function (event, data) {
            $http.post(urlActions.saveSession, data).success(function () {
                $rootScope.publish('sessionSaved');
                local.startNewSession();
            });
        });

        $http.get(urlActions.cardRooms).success(function (data) {
            $scope.cardRooms = data;
        });

        $http.get(urlActions.games).success(function (data) {
            $scope.rooms = data;
        });

        local.startNewSession();
    }
]);