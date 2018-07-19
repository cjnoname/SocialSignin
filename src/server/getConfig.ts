import { Config, Theme } from './models/config';
import { config, DotenvResult } from 'dotenv';

const dotenvResult: DotenvResult = config();

const themes: Theme = JSON.parse(dotenvResult.parsed!.theme);

const configs: Config = { themes };

export { configs };
