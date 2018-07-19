import server from 'utils/server';

export const signIn = <T>(data: any) => server.post<T>('account/signIn', data);
