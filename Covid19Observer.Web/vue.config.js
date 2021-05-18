module.exports = {
    devServer: {
        proxy : 'http://localhost:3000'
    },
    publicPath: process.env.NODE_ENV == 'production' ? 'https://jmrrgncpz.github.io/COVID19Observer' : '/'
}