import { Request, Response } from 'express';
import * as React from 'react';
import { Provider } from 'react-redux';
import { StaticRouter } from 'react-router-dom';
import { renderToString } from 'react-dom/server';
import { createMemoryHistory } from 'history';
import { SheetsRegistry } from 'jss';
import { JssProvider } from 'react-jss';
import configureStore from 'client/configureStore';
import { MuiThemeProvider, createMuiTheme, createGenerateClassName } from '@material-ui/core/styles';
import { routes } from 'client/routes';
import { InitialState } from 'models/initial';
import { SignInState } from 'models/signIn';
import { SignUpState } from 'models/signUp';
import { ForgotPasswordState } from 'models/forgotPassword';

export const renderPage = async (req: Request, res: Response, initialState: InitialState) => {
  const store = configureStore(createMemoryHistory(),
    { initial: new InitialState(initialState), forgotPassword: new ForgotPasswordState(), signIn: new SignInState(), signUp: new SignUpState() });
  const sheetsRegistry = new SheetsRegistry();
  const generateClassName = createGenerateClassName();
  const ui = (
    <JssProvider registry={sheetsRegistry} generateClassName={generateClassName}>
      <Provider store={store}>
        <StaticRouter location={req.url} context={{}} children={routes} />
      </Provider>
    </JssProvider>
  );
  const html = renderToString(ui);
  const css = sheetsRegistry.toString();

  res.send(renderFullPage(html, css));
};

const renderFullPage = (html: any, css: any) => {
  return `
      <!doctype html>
      <html>
        <head>
          <meta charset="utf-8" />
          <meta name="viewport" content="width=device-width, initial-scale=1.0" />
          <title></title>
          <base href="/" />
          <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
          <link rel="shortcut icon" href="favicon.ico">
          <link rel="stylesheet" type="text/css" href="css/global.css">
        </head>
        <body>
          <script src="https://apis.google.com/js/platform.js?onload=googleAsyncInit" defer></script>
          <script>
            window.googleAsyncInit = function () {
              gapi.load('auth2', function () {
                window.GoogleAuth = gapi.auth2.init({
                  client_id: '23587345422-gdsfcf8nnhr1q5kitp969eb36qf7meqr.apps.googleusercontent.com'
                });
              });
            }
          </script>
          <script>
            window.fbAsyncInit = function () {
              FB.init({
                appId: '1844014309225868',
                cookie: true,
                xfbml: true,
                version: 'v2.12',
                status: true
              });
              FB.AppEvents.logPageView();
              window.FB = FB;
            };
            (function (d, s, id) {
              var js, fjs = d.getElementsByTagName(s)[0];
              if (d.getElementById(id)) { return; }
              js = d.createElement(s); js.id = id;
              js.src = "https://connect.facebook.net/en_US/sdk.js";
              fjs.parentNode.insertBefore(js, fjs);
            }(document, 'script', 'facebook-jssdk'));
          </script>
          <div id="react-app">${html}</div>
          <style id="jss-server-side">${css}</style>
          <script type="text/javascript" src="vendor.js"></script>
          <script type="text/javascript" src="client.js"></script>
        </body>
      </html>
    `;
};
