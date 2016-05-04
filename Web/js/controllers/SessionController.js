angular.module('pokerTracker', [])
    .controller('SessionController', ['$scope', '$rootScope', '$http', '$ms', 'actionUrls',
    function ($scope, $rootScope, $http, $ms, actionUrls) {
        function startNewSession() {
            $http.get(urlActions.createSession).success(function (data) {
                data.StartingStackSize = null;
                $scope.session = data;
                $('#frmSession').removeClass('hidden');
            });
        }

        $scope.startSession = function () {
            $rootScope.$emit('sessionStarted', $scope.session);
            $('#frmSession').addClass('hidden');
        };

        $scope.saveSession = function () {
            $http.post(urlActions.saveSession, data).success(function () {
                startNewSession();
                $rootScope.$emit('sessionSaved');
                $('#dlgFinished').foundation('close');
            });
        };

        $scope.cancelSession = function () {
            startNewSession();
            $rootScope.$emit('sessionCanceled');
            $('#dlgCancel').foundation('close');
        };

        $http.get(urlActions.cardRooms).success(function (data) {
            $scope.cardRooms = data;
        });

        $http.get(urlActions.games).success(function (data) {
            $scope.rooms = data;
        });

        startNewSession();
    }
]);