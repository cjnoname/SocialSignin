import { Record } from 'immutable';

interface Interface {
  isLoading: boolean
}

const initialValue = Record<Interface>({
  isLoading: false
});

export class SignUpState extends initialValue { }
