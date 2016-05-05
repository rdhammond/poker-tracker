angular.module('pokerTracker.controllers', ['pokerTracker.services'])
    .controller('SessionController', ['$scope', '$rootScope', 'Session', 'CardRooms', 'GameTypes', 'actionUrls',
    function ($scope, $rootScope, Session, CardRooms, GameTypes, actionUrls) {
        $scope.isShown = true;

        $scope.startSession = function () {
            $scope.isShown = false;
            $rootScope.$emit('sessionStarted', $scope.session);
        };

        $scope.cancelSession = function() {
            Session.createSession().then(function (sesssion) {
                $scope.session = session;
                $rootScope.$emit('sessionCanceled');
            });
        };

        $rootScope.$on('saveSession', function (event, data) {
            Session.saveSession(session).then(function (newSession) {
                $scope.session = newSession;
                $rootScope.$emit('sessionSaved');
            });
        });

        CardRooms.get().then(function (cardRooms) {
            $scope.cardRooms = cardRooms;
        });

        Games.get().then(function (games) {
            $scope.games = games;
        })

        Session.createSession().then(function (session) {
            $scope.session = session;
        });
    }
]);