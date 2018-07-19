
export const SocialButtonStyle = (theme: any) => {
  return ({
    buttonContainer: {
      textAlign: 'right' as 'right',
      width: '100%',
      marginTop: '2em',
    },
    button: {
      color: '#fff',
      textTransform: 'none' as 'none',
      fontWeight: 600 as 600,
      fontSize: '0.9em',
      height: '3.2em'
    },
    icon: {
      fontSize: 26
    },
    text: {
      width: `calc(100% - 26px)`
    }
  });
};
