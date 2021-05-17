module.exports = {
    devServer: {
        proxy : 'http://localhost:30193'
    },
    publicPath: process.env.NODE_ENV == 'production' ? 'https://jmrrgncpz.github.io/COVID19Observer' : '/'
}