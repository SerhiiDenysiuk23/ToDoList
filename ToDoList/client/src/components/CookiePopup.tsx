import React from 'react';

const CookiePopup = () => {
    const [showPopup, setShowPopup] = React.useState(false);

    React.useEffect(() => {
        const cookieConsent = getCookie("cookieConsent");
        if (!cookieConsent) {
            setShowPopup(true);
        }
    }, []);

    const acceptCookies = () => {
        setCookie("cookieConsent", "true", 365);
        setShowPopup(false);
    };

    const setCookie = (name: string, value: string, days: number) => {
        const expires = new Date();
        expires.setTime(expires.getTime() + days * 24 * 60 * 60 * 1000);
        document.cookie = `${name}=${value}; expires=${expires.toUTCString()}; path=/`;
    };

    const getCookie = (name: string) => {
        const nameEQ = `${name}=`;
        const cookies = document.cookie.split(";");
        for (let i = 0; i < cookies.length; i++) {
            let cookie = cookies[i];
            while (cookie.charAt(0) === " ") {
                cookie = cookie.substring(1, cookie.length);
            }
            if (cookie.indexOf(nameEQ) === 0) {
                return cookie.substring(nameEQ.length, cookie.length);
            }
        }
        return null;
    };

    if (!showPopup) {
        return null;
    }

    return (
        <div id="cookiePopup" className="cookie-popup">
            <div className="cookie-popup-content">
                <span className="cookie-popup-message">We use cookies to improve your experience on the site. By continuing to use our site, you agree to our <a
                    href="">cookie policy</a>.</span>
                <button onClick={acceptCookies} id="cookieAccept" className="cookie-popup-button">Agree</button>
            </div>
        </div>
    );
};

export default CookiePopup;