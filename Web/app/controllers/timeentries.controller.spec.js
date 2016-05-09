describe('TimeEntriesController', function () {
    var msDate,
        $rootScope, $controller,
        controller;

    beforeEach(module('app'));

    beforeEach(module(function ($provide) {
        msDate = new msDateMock();

        $provide.value('msDate', msDate);

        function msDateMock() {
            return {
                from: function (date) {
                    return 'mock';
                }
            };
        }
    }));

    beforeEach(inject(function (_$rootScope_, _$controller_) {
        $rootScope = _$rootScope_;
        $controller = _$controller_;
    }));

    beforeEach(function () {
        controller = $controller('TimeEntriesController', { $scope: $rootScope.$new() });
    });

    describe('Constructor', function () {
        it('is initially hidden', function () {
            expect(controller.isShown).toBe(false);
        });

        it('initializes new time entry', function () {
            expectDeepEqual(controller.timeEntry, {});
        })
    });

    describe('#addTimeEntry', function () {
        it('emits addTimeEntry w/timeEntry and RecordedAt', function () {
            var emitSpy = spyOn($rootScope, '$emit').and.callThrough();

            $rootScope.$on('addTimeEntry', function (event, data) {
                expect(data.RecordedAt).toBe('mock');
                expect(data.test).toBe(1);
            });

            controller.timeEntry = { test: 1 };
            controller.addTimeEntry();
            expect($rootScope.$emit).toHaveBeenCalled();
            expect(emitSpy.calls.mostRecent().args[0]).toBe('addTimeEntry');
        });

        it('resets timeEntry', function () {
            controller.timeEntry = { test: 1 };
            controller.addTimeEntry();
            expectDeepEqual(controller.timeEntry, {});
        });
    });

    describe('Events', function () {
        describe('#sessionStarted', function () {
            it('emits addTimeEntry w/initial entry', function () {
                var emitSpy = spyOn($rootScope, '$emit').and.callThrough();

                $rootScope.$on('addTimeEntry', function (event, data) {
                    expect(data.RecordedAt).toBe('mock');
                    expect(data.StackSize).toBe(100);
                    expect(data.DealerTokes).toBe(0);
                    expect(data.ServerTips).toBe(0);
                });

                $rootScope.$emit('sessionStarted', { StartingStackSize: 100 });
                expect($rootScope.$emit).toHaveBeenCalled();
                expect(emitSpy.calls.mostRecent().args[0]).toBe('addTimeEntry');
            });

            it('shows form', function () {
                controller.isShown = false;
                $rootScope.$emit('sessionStarted', { StartingStackSize: 100 });
                expect(controller.isShown).toBe(true);
            });
        });

        describe('#sessionFinished', function () {
            it('resets timeEntry', function () {
                controller.timeEntry = { test: 1 };
                $rootScope.$emit('sessionFinished');
                expectDeepEqual(controller.timeEntry, {});
            });

            it('hides form', function () {
                controller.isShown = true;
                $rootScope.$emit('sessionFinished');
                expectDeepEqual(controller.timeEntry, {});
            });
        });

        describe('#sessionCanceled', function () {
            it('resets timeEntry', function () {
                controller.timeEntry = { test: 1 };
                $rootScope.$emit('sessionCanceled');
                expectDeepEqual(controller.timeEntry, {});
            });

            it('hides form', function () {
                controller.isShown = true;
                $rootScope.$emit('sessionCanceled');
                expectDeepEqual(controller.timeEntry, {});
            });
        });
    });

    function expectDeepEqual(actual, expected) {
        return JSON.stringify(actual) === JSON.stringify(expected);
    }
});