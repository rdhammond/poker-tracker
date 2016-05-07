(function () {
    'use strict';
    
    angular
        .module('app')
        .controller('session', session);

    session.$inject = ['$rootScope', 'cardRooms', 'gameTyes', 'session', 'msDate'];

    function session($rootScope, cardRooms, gameTypes, session, msDate) {
        var vm = this;
        vm.isShown = false;
        vm.session = {};
        vm.startSession = startSession;
        vm.saveSession = saveSession;
        vm.cancelSession = cancelSession;

        $rootScope.$on('addTimeEntry', addTimeEntry);

        activate();

        function startSession() {
            vm.session.StartDate = msDate.now();
            vm.isShown = false;
            $rootScope.$emit('sessionStarted', vm.StartingStackSize);
        }

        function saveSession() {
            vm.session.EndDate = msDate.now();

            session
                .saveSession(vm.session)
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

        function addTimeEntry(event, date) {
            vm.session.TimeEntries = vm.session.TimeEntries || [];
            vm.session.TimeEntries.push(data.timeEntry);
        }

        function activate() {
            cardRooms
                .get()
                .then(function (cardRooms) { vm.cardRooms = cardRooms; });

            gameTypes
                .get()
                .then(function (gameTypes) { vm.gameTypes = gameTypes; });

            reset();
        }

        function reset() {
            vm.session = {};
            delete vm.StartingStackSize;
        }
    }
})();