describe('session', function () {
    var $httpBackend, session, urlActions;

    beforeEach(module('app'));

    beforeEach(inject(function (_$httpBackend_, _session_, _urlActions_) {
        $httpBackend = _$httpBackend_;
        session = _session_;
        urlActions = _urlActions_;
    }));

    describe('#save', function () {
        it('should post session info', function () {
            $httpBackend
                .expect('POST', urlActions.saveSession)
                .respond(200);

            session.save({});
            $httpBackend.flush();
            $httpBackend.verifyNoOutstandingExpectation();
        });
    });
});