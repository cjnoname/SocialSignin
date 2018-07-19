
import { Request } from 'express';
import server from 'sharedUtils/server';
import { InitialState } from 'models/initial';
import { Theme } from '../models/config';

export const fetchTheme = (async (req: Request, themes: Theme) => {
  try {
    const path = req.get('host')!.split('.')[0];
    const url = themes[path] ? themes[path] : themes['default'];
    return await server.simpleGet<InitialState>(url);
  } catch (e) {
    console.log(e);
  }
});
