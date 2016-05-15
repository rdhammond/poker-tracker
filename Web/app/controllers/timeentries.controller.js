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