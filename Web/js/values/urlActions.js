angular.module('pokerTracker.values', [])
    .value('actionUrls', {
        getCardRooms: 'PokerTrackerService.svc/CardRooms',
        getGames: 'PokerTrackerService.svc/GameTypes',
        createSession: 'PokerTrackerService.svc/Session/New',
        saveSession: 'PokerTrackerService.svc/Session'
    });