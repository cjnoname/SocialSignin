import server from 'utils/server';

export const signUp = <T>(data: any) => server.post<T>('signIn/signUp', data);
