import { Record } from 'immutable';
import { blue, green, purple, yellow } from '@material-ui/core/colors';

interface Interface {
  primaryColor: string,
  secondaryColor: string
}

const initialValue = Record<Interface>({
  primaryColor: '#1badeb',
  secondaryColor: green[500]
});

export class Theme extends initialValue { }
