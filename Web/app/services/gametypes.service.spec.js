describe('gameTypes', function () {
    var urlActions, $httpBackend, gameTypes;

    beforeEach(module('app'));

    beforeEach(inject(function (_$httpBackend_, _gameTypes_, _urlActions_) {
        $httpBackend = _$httpBackend_;
        gameTypes = _gameTypes_;
        urlActions = _urlActions_;
    }));

    describe('#get', function () {
        it('returns list from service', function () {
            var expected = [{ test: 1 }],
                checkResultsWrapper = { checkResults: checkResults },
                checkResultsSpy = spyOn(checkResultsWrapper, 'checkResults').and.callThrough();

            $httpBackend
                .expect('GET', urlActions.getGameTypes)
                .respond(expected);

            gameTypes.get().then(checkResultsWrapper.checkResults);
            $httpBackend.flush();
            $httpBackend.verifyNoOutstandingExpectation();
            expect(checkResultsWrapper.checkResults).toHaveBeenCalled();

            function checkResults(response) {
                expectDeepEqual(response, expected);
            }
        });
    });

    function expectDeepEqual(actual, expected) {
        return JSON.stringify(actual) === JSON.stringify(expected);
    }
});
