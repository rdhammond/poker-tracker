angular.module('pokerTracker', [])
    .value('actionUrls', {
        cardRooms: 'PokerTrackerService.svc/CardRooms',
        games: 'PokerTrackerService.svc/GameTypes',
        createSession: 'PokerTrackerService.svc/Session/New',
        saveSession: 'PokerTrackerService.svc/Session'
    });