import * as Koa from 'koa';
import * as Router from 'koa-router';
import * as Middlewares from './middlewares';
import * as Controllers from './controllers';
import { InitialState } from 'models/initial';
import { configs } from './getConfig';
import * as Static from 'koa-static';

const app = new Koa();
app.use(Static('static'));

let initialState: InitialState;
app.use(async (ctx, next) => {
  if (ctx.request.originalUrl.includes('/api/theme') || !ctx.request.originalUrl.includes('/api/')) {
    initialState = await Middlewares.fetchTheme(ctx, configs.themes);
  }
  next();
});

const home = new Router();
home.get('*', async (ctx) => {
  ctx.body = await Controllers.renderPage(ctx, initialState);
});

const api = new Router();
api.get('/theme', async (ctx) => {
  ctx.body = initialState;
});

const router = new Router();
router.use('/api', api.routes(), api.allowedMethods());
router.use('*', home.routes(), home.allowedMethods());

app.use(router.routes()).use(router.allowedMethods());

export { app };
