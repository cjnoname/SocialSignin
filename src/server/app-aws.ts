import * as express from 'express';
import * as Middlewares from './middlewares';
import * as Controllers from './controllers';
import { InitialState } from 'models/initial';
import { configs } from './getConfig';

const app = express();
app.use(express.static('static'));

let initialState: InitialState;
app.use(async (req, res, next) => {
  if (req.originalUrl.includes('/api/theme') || !req.originalUrl.includes('/api/')) {
    initialState = await Middlewares.fetchTheme(req, configs.themes);
  }
  next();
});

app.get('/api/theme', (req, res) => res.send(initialState));

app.get('*', (req, res) => Controllers.renderPage(req, res, initialState));

export default app;
