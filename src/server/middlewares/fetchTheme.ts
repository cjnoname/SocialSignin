
import { Request } from 'koa';
import server from 'sharedUtils/server';
import { InitialState } from 'models/initial';
import { Theme } from '../models/config';
import { Context } from 'koa';

export const fetchTheme = (async (ctx: Context, themes: Theme) => {
  try {
    const path = ctx.request.get('host')!.split('.')[0];
    const url = themes[path] ? themes[path] : themes['default'];
    return await server.simpleGet<InitialState>(url);
  } catch (e) {
    console.log(e);
  }
});
