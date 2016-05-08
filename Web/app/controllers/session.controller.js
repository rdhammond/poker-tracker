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
            vm.session.StartDate = msDate.now();
            vm.isShown = false;
            $rootScope.$emit('sessionStarted', { StartingStackSize: vm.StartingStackSize });
        }

        function saveSession() {
            vm.session.EndDate = msDate.now();

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
            vm.session.TimeEntries.push(data.timeEntry);
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