(function () {
    'use strict';

    angular
        .module('app')
        .controller('TimeEntries', TimeEntries);

    TimeEntries.$inject = ['$rootScope', msDate];

    function TimeEntries($rootScope, msDate) {
        var vm = $this;
        vm.isShown = false;
        vm.timeEntry = {};
        vm.addTimeEntry = addTimeEntry;

        $rootScope.$on('sessionStarted', addInitialEntry);
        $rootScope.$on('sessionFinished', resetViewModel);
        $rootScope.$on('sessionCanceled', resetViewModel);

        function addTimeEntry() {
            vm.timeEntry.RecordedAt = msDate.now();
            $rootScope.$emit('addTimeEntry', vm.timeEntry);
            vm.timeEntry = {};
        }

        function addInitialEntry(event, data) {
            $rootScope.$emit('addTimeEntry', {
                RecordedAt: msDate.now(),
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
    }
})();