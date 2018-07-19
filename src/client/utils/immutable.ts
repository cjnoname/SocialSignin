import { List } from 'immutable';

export const toImmutable = (value: any, Class: any) => {
  if (Class === 'string') {
    return (value && value.length > 0) ? List<string>(value) : List<string>();
  } else if (value instanceof Array) {
    return (value && value.length > 0) ? List(value.map((val: any) => new Class(val))) : List();
  } else {
    return value ? new Class(value) : new Class();
  }
};
