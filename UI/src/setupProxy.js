
const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/api",
];

const contextProlog =[
    "/prolog",
];

const contextPost =[
    "/posts",
];

module.exports = function(app) {

    const appProxyMDRS = createProxyMiddleware(context,{
        target: 'https://localhost:5001',
        changeOrigin: true,
        secure: false,
    });

    const appProxyProlog = createProxyMiddleware(contextProlog,{
        target: 'http://localhost:5002',
        changeOrigin: true,
        secure: false,
    })

    const appProxyPost = createProxyMiddleware(contextPost,{
        target: 'http://localhost:5003',
        changeOrigin: true,
        secure: false,
    })

    app.use(appProxyMDRS);
    app.use(appProxyProlog);
    app.use(appProxyPost);
};
