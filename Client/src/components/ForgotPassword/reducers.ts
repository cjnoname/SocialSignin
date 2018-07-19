import { Action, Reducer, ActionCreator } from 'redux';
import { KnownAction, ActionTypes } from './actions';
import * as reducer from './workers/forgotPassword';
import { ForgotPasswordState } from 'models/forgotPassword';

export const ForgotPasswordReducer: Reducer<ForgotPasswordState> = (state: ForgotPasswordState, incomingAction: Action) => {
  const action = incomingAction as KnownAction;
  switch (action.type) {
    case ActionTypes.FORGOT_PASSWORD_STARTED: { return reducer.forgotPasswordStarted(state); }
    case ActionTypes.FORGOT_PASSWORD_FAILED: { return reducer.forgotPasswordFailed(state); }
  }
  return state || new ForgotPasswordState();
};
