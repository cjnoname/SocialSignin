import { IException, isNoContent } from 'utils/error';

const sharedHeaders = {
  Accept: 'application/json',
  'Content-Type': 'application/json'
};

enum HttpMethods {
  GET = 'GET',
  POST = 'POST',
  PUT = 'PUT',
  PATCH = 'PATCH',
  DELETE = 'DELETE'
}

const buildURL = (path: string) => {
  if (path.indexOf('?') > 0) {
    return `api/${path}&nocache=${Math.random()}`;
  }
  return `api/${path}?nocache=${Math.random()}`;
};

const get = <T>(path: string, data?: any, options = {}): Promise<T | undefined> => {
  path = data ? `${path}?${getQueryString(data)}` : path;
  return doFetch(path, null, HttpMethods.GET, options);
};

const getQueryString = (params: any) => (
  Object
    .keys(params)
    .map(k => {
      if (Array.isArray(params[k])) {
        return params[k]
          .map((val: any) => `${encodeURIComponent(k)}[]=${encodeURIComponent(val)}`)
          .join('&');
      }

      return `${encodeURIComponent(k)}=${encodeURIComponent(params[k])}`;
    })
    .join('&'));

const post = <T>(path: string, data: any, options = {}): Promise<T | undefined> => (
  doFetch(path, data, HttpMethods.POST, options));

const put = <T>(path: string, data: any, options = {}): Promise<T | undefined> => (
  doFetch(path, data, HttpMethods.PUT, options));

const del = <T>(path: string, data: any, options = {}): Promise<T | undefined> =>
  (doFetch(path, data, HttpMethods.DELETE, options));

const doFetch = async (path: string, data: any, method: HttpMethods, options = {}) => {
  const request = {
    method,
    headers: {
      ...options,
      ...sharedHeaders
    }
  } as any;
  if (method !== HttpMethods.GET) {
    request.body = data ? JSON.stringify(data) : null;
  }
  try {
    const response = await fetch(buildURL(path), request);
    if (!response.ok) {
      const error: IException = { httpCode: response.status, message: await response.json() };
      throw error;
    }
    return isNoContent(response) ? undefined : await response.json();
  } catch (e) {
    if (e.httpCode) {
      // console.log(e.message);
    }
    // console.log(e);
  }
};

export default {
  buildURL,
  get,
  post,
  put,
  delete: del
};
