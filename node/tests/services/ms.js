describe('ms', function() {
  var ms;
  
  beforeEach(module('pokerTracker.services'));
  beforeEach(inject(function (_ms_) {
    ms = _ms_;
  }));
  
  describe('date', function() {
    it('should convert javascript dates', function() {
      // 2016-05-05T02:46:35.833Z
      var time = 1462416395833;
      var expected = '/Date(' + time.toString() + ')/';
      
      expect(ms.date(new Date(time))).to.equal(expected);
    });
  });
});