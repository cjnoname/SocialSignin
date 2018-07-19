import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from 'store';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import { getTheme } from 'shared/theme';
import { Theme } from 'models/initial';

const Component = React.PureComponent;

function withRoot(Component: any) {
  function WithRoot(props: any) {
    return (
      <MuiThemeProvider theme={getTheme(props.theme)}>
        <Component {...props} />
      </MuiThemeProvider>
    );
  }
  return connect(
    (state: ApplicationState) => ({
      theme: state.initial.theme
    })
  )(WithRoot);
}

export { withRoot };
