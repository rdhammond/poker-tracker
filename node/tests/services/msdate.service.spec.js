describe('msDate', function() {
  var msDate;
  
  beforeEach(module('app'));
  
  beforeEach(inject(function($injector) {
    msDate = $injector.get('msDate');
  }));
  
  describe('now', function() {
    it('should return MS formatted date', function() {
      assert.isTrue(/^\/Date(.*?)\/$/.test(msDate.now()));
    });
  });
});