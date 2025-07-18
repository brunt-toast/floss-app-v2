window.setCookie = function(name, value, days) {
    console.log(`JS: Set cookie ${name} to ${value}`);
    let expires = "";
    if (days) {
        const date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

window.getCookie = function(name) {
    const nameEQ = name + "=";
    const ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i].trim();
        if (c.indexOf(nameEQ) === 0) {
            let ret = c.substring(nameEQ.length, c.length);
            console.log(`JS: Get cookie ${name} as ${ret}`);
            return ret;
        }
    }
    return null;
}
