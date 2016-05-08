describe('SessionController', function () {
    var CARD_ROOMS = [],
        GAME_TYPES = []

    var cardRooms, games, msDate, session,
            $q, $rootScope, $controller,
            controller;

    beforeEach(module('app'));

    beforeEach(module(function ($provide) {
        cardRooms = new cardRoomsMock();
        gameTypes = new gameTypesMock();
        msDate = new msDateMock();
        session = new sessionMock();

        $provide.value('cardRooms', cardRooms);
        $provide.value('gameTypes', gameTypes);
        $provide.value('msDate', msDate);
        $provide.value('session', session);

        function cardRoomsMock() {
            return {
                get: function () {
                    var deferred = $q.defer();
                    deferred.resolve(CARD_ROOMS);
                    return deferred.promise;
                }
            };
        }

        function gameTypesMock() {
            return {
                get: function () {
                    var deferred = $q.defer();
                    deferred.resolve(GAME_TYPES);
                    return deferred.promise;
                }
            };
        }

        function sessionMock() {
            return {
                save: function (sessionInfo) {
                    var deferred = $q.defer();
                    deferred.resolve();
                    return deferred.promise;
                }
            };
        }

        function msDateMock() {
            return {
                now: function () {
                    return 'mock';
                }
            };
        }
    }));

    beforeEach(inject(function (_$rootScope_, _$controller_, _$q_) {
        $rootScope = _$rootScope_;
        $controller = _$controller_;
        $q = _$q_;
    }));

    beforeEach(function () {
        controller = $controller('SessionController', { $scope: $rootScope.$new() });
    });

    describe('Constructor', function () {
        it('loads card rooms', function () {
            $rootScope.$digest();
            expect(controller.cardRooms).toBe(CARD_ROOMS);
        });

        it('loads game types', function () {
            $rootScope.$digest();
            expect(controller.gameTypes).toBe(GAME_TYPES);
        });

        it('shows form after', function () {
            $rootScope.$digest();
            expect(controller.isShown).toBe(true);
        });
    });

    describe('#startSession', function () {
        it('sets session start date', function () {
            controller.startSession();
            expect(controller.session.StartDate).toBe('mock');
        });

        it('hides form after', function () {
            controller.startSession();
            expect(controller.isShown).toBe(false);
        });

        it('emits sessionStarted w/StartingStackSize', function () {
            var emitSpy = spyOn($rootScope, '$emit').and.callThrough();
            controller.StartingStackSize = 100;

            $rootScope.$on('sessionStarted', function (event, data) {
                expect(data.StartingStackSize).toBe(100);
            });

            controller.startSession();
            expect($rootScope.$emit).toHaveBeenCalled();
            expect(emitSpy.calls.mostRecent().args[0]).toBe('sessionStarted');
        });
    });

    describe('#saveSession', function () {
        it('initially sets session end date', function () {
            controller.saveSession();
            // NO $digest here!!!

            expect(controller.session.EndDate).toBe('mock');
        });

        it('saves session via service', function () {
            var saveSpy = spyOn(session, 'save').and.callThrough();

            controller.saveSession();
            $rootScope.$digest();

            expect(session.save).toHaveBeenCalled();
        });

        it('resets session after save', function () {
            controller.session = { mock: 1 };
            controller.StartingStackSize = 100;

            controller.saveSession();
            $rootScope.$digest();

            expect(controller.StartingStackSize).not.toBeDefined();
            expect(controller.session.mock).not.toBeDefined();
        });

        it('makes form visible after save', function () {
            controller.saveSession();
            $rootScope.$digest();

            expect(controller.isShown).toBe(true);
        });

        it('emits sessionSaved', function () {
            var emitSpy = spyOn($rootScope, '$emit').and.callThrough();

            controller.saveSession();
            $rootScope.$digest();

            expect($rootScope.$emit).toHaveBeenCalled();
            expect(emitSpy.calls.mostRecent().args[0]).toBe('sessionSaved');
        });
    });

    describe('#cancelSession', function () {
        it('resets session afterwards', function () {
            controller.session = { mock: 1 };
            controller.StartingStackSize = 100;

            controller.cancelSession();
            expect(controller.session.mock).not.toBeDefined();
            expect(controller.StartingStackSize).not.toBeDefined();
        });

        it('makes form visible afterwards', function () {
            controller.cancelSession();
            expect(controller.isShown).toBe(true);
        });

        it('emits sessionCanceled', function () {
            var emitSpy = spyOn($rootScope, '$emit').and.callThrough();

            controller.cancelSession();
            expect($rootScope.$emit).toHaveBeenCalled();
            expect(emitSpy.calls.mostRecent().args[0]).toBe('sessionCanceled');
        });
    });

    describe('Event: addTimeEntry', function () {
        it('initializes entries if first added', function () {
            $rootScope.$emit('addTimeEntry', { timeEntry: 'test' });
            expect(controller.session.TimeEntries).toBeDefined();
            expect(controller.session.TimeEntries.length).toBe(1);
            expect(controller.session.TimeEntries).toContain('test');
        });

        it('adds to existing entries', function () {
            controller.session.TimeEntries = ['test1'];

            $rootScope.$emit('addTimeEntry', { timeEntry: 'test2' });
            expect(controller.session.TimeEntries.length).toBe(2);
            expect(controller.session.TimeEntries).toContain('test2');
        });
    });
});