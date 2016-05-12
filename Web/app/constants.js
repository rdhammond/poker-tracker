(function () {
    'use strict';

    // ** DEBUG
    var ROOT_URL = 'http://localhost:53468/PokerTrackerService.svc/';

    angular
        .module('app')
        .constant('urlActions', {
            getCardRooms: ROOT_URL + 'CardRooms',
            getGameTypes: ROOT_URL + 'GameTypes',
            saveSession: ROOT_URL + 'Session'
        });
})();