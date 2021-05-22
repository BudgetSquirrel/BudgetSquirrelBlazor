(function() {

  class BrowserService {

    /**
     * Sets a cookie with the given name and value and configures it to expire
     * after the given number of hours.
     * @param {string} name the name of the cookie to set
     * @param {string} value the value of the cookie to set
     * @param {int} hours number of hours until the cookie expires
     */
    setCookie(name, value, hours) {
      let expires;
      if (hours) {
          let date = new Date();
          date.setTime(date.getTime() + (hours * 60 * 60 * 1000));
          expires = "; expires=" + date.toGMTString();
      }
      else {
          expires = "";
      }
      document.cookie = name + "=" + value + expires + "; path=/";
    }

    /**
     * Returns an object that represents the cookie with the given name. If the cookie doesn't exist,
     * null is returned.
     * @param {string} name the name of the cookie
     * @returns an object with the name and value of the cookie in the keys 'name' and 'value'. If
     * cookie doesn't exist, null is returned.
     */
    getCookie(name) {
      const cookies = document.cookie.split(";").map(cookie => _parseCookie(cookie));
      const desiredCookie = cookies.find(c => c.name == name);
      if (desiredCookie) {
        return desiredCookie.value;
      } else {
        return null;
      }
    }
  }

  /**
   * Returns an object that represents the given individual cookie. This cookie is not
   * the full string in document.cookie but a single individual cookie after splitting
   * the full cookie string by semicolon.
   * @param {string} cookie the unparsed individual cookie from document.cookie (format 'cookieName=cookieValue')
   * @returns an object with the name and value of the cookie in the keys 'name' and 'value'.
   */
  function _parseCookie(cookie) {
    return {
      name: cookie.split("=")[0].trim(),
      value: cookie.split("=")[1].trim()
    };
  }

  const browserService = new BrowserService();

  window.browserService = browserService;

})();