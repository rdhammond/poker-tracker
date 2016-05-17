(function () {
    'use strict';

    angular
        .module('app', [])
        .run(function() {
            $(function () {
                var dlgFinish = new Foundation.Reveal($('#dlgFinish')),
                    dlgCancel = new Foundation.Reveal($('#dlgCancel'));
            });
        });
})();
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
(function () {
    'use strict';

    angular
        .module('app')
        .directive('zfDialog', zfDialog);

    function zfDialog() {
        return {
            restrict: 'E',
            transclude: true,
            replace: true,
            templateUrl: 'views/zf-dialog.html'
        };
    }
})();
(function () {
    'use strict';

    angular
        .module('app')
        .service('cardRooms', cardRooms);

    cardRooms.$inject = ['$http', 'urlActions'];

    function cardRooms($http, urlActions) {
        return { get: get };

        function get() {
            return $http
                .get(urlActions.getCardRooms)
                .then(function (response) { return response.data; });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('app')
        .service('gameTypes', gameTypes);

    gameTypes.$inject = ['$http', 'urlActions'];

    function gameTypes($http, urlActions) {
        return { get: get };

        function get() {
            return $http
                .get(urlActions.getGameTypes)
                .then(function(response) { return response.data; });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('app')
        .service('msDate', msDate);

    function msDate() {
        return { from: from };

        function from(date) {
            return '/Date(' + date.getTime() + '+0000)/';
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('app')
        .service('session', session);

    session.$inject = ['$http', 'urlActions'];

    function session($http, urlActions) {
        return { save: save };

        function save(sessionInfo) {
            return $http.post(urlActions.saveSession, sessionInfo);
        }
    }
})();
(function () {
    'use strict';
    
    angular
        .module('app')
        .controller('SessionController', SessionController);

    SessionController.$inject = ['$rootScope', 'cardRooms', 'gameTypes', 'session', 'msDate'];

    function SessionController($rootScope, cardRooms, gameTypes, session, msDate) {
        var vm = this;
        vm.isShown = true;
        vm.session = {};
        vm.startSession = startSession;
        vm.saveSession = saveSession;
        vm.cancelSession = cancelSession;

        $rootScope.$on('addTimeEntry', addTimeEntry);

        activate();

        function startSession() {
            vm.session.StartTime = msDate.from(new Date());
            vm.isShown = false;
            $rootScope.$emit('sessionStarted', { StartingStackSize: vm.StartingStackSize });
        }

        function saveSession() {
            vm.session.EndTime = msDate.from(new Date());

            return session
                .save(vm.session)
                .then(function () {
                    reset();
                    vm.isShown = true;
                    $rootScope.$emit('sessionSaved');
                });
        }

        function cancelSession() {
            reset();
            vm.isShown = true;
            $rootScope.$emit('sessionCanceled');
        }

        function addTimeEntry(event, data) {
            vm.session.TimeEntries = vm.session.TimeEntries || [];
            vm.session.TimeEntries.push(data);
        }

        function activate() {
            cardRooms
                .get()
                .then(function (cardRooms) { vm.cardRooms = cardRooms; });

            gameTypes
                .get()
                .then(function (gameTypes) { vm.gameTypes = gameTypes; });
        }

        function reset() {
            vm.session = {};
            delete vm.StartingStackSize;
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('app')
        .controller('TimeEntriesController', TimeEntriesController);

    TimeEntriesController.$inject = ['$rootScope', 'msDate'];

    function TimeEntriesController($rootScope, msDate) {
        var vm = this;
        vm.isShown = false;
        vm.timeEntry = {};
        vm.addTimeEntry = addTimeEntry;
        vm.openFinishDialog = openFinishDialog;
        vm.openCancelDialog = openCancelDialog;

        $rootScope.$on('sessionStarted', addInitialEntry);
        $rootScope.$on('sessionFinished', resetViewModel);
        $rootScope.$on('sessionCanceled', resetViewModel);
        $rootScope.$on('sessionSaved', function () { vm.isShown = false; });

        function addTimeEntry() {
            vm.timeEntry.RecordedAt = msDate.from(new Date());
            $rootScope.$emit('addTimeEntry', vm.timeEntry);
            vm.timeEntry = {};
        }

        function addInitialEntry(event, data) {
            $rootScope.$emit('addTimeEntry', {
                RecordedAt: msDate.from(new Date()),
                StackSize: data.StartingStackSize,
                DealerTokes: 0,
                ServerTips: 0
            });

            vm.isShown = true;
        }

        function resetViewModel() {
            vm.timeEntry = {};
            vm.isShown = false;
        }

        function openFinishDialog() {
            $('#dlgFinish').foundation('open');
        }

        function openCancelDialog() {
            $('#dlgCancel').foundation('open');
        }
    }
})();