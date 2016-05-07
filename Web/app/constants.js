(function () {
    'use strict';

    angular
        .module('app')
        .constant('urlActions', {
            getCardRooms: 'PokerTrackerService.svc/CardRooms',
            getGameTypes: 'PokerTrackerService.svc/GameTypes',
            saveSession: 'PokerTrackerService.svc/Session'
        });
})();